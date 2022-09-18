using System;
using UniRx;

namespace ThirdParty.EventBus
{
    public interface IEventBus
    {
        void Send(int id);
        void Send(string id);
        void Send(object id);

        void Send<TPayload>(int id, TPayload payload);
        void Send<TPayload>(string id, TPayload payload);
        void Send<TPayload>(object id, TPayload payload);

        IObservable<int> WasTriggered(int id);
        IObservable<string> WasTriggered(string id);
        IObservable<object> WasTriggered(object id);


        IObservable<(int, TPayload)> WasTriggered<TPayload>(int id);
        IObservable<(string, TPayload)> WasTriggered<TPayload>(string id);
        IObservable<(object, TPayload)> WasTriggered<TPayload>(object id);

    }

    public class EventBus : IEventBus
    {
        private Subject<(int, object)> _sendInt = new();
        private Subject<(int, object)> _sendString = new();
        private Subject<(object, object)> _sendObject = new();

        public void Send(int id)
        {
            _sendInt.OnNext((id, null));
        }

        public void Send(string id)
        {
            _sendString.OnNext((id.GetHashCode(), null));
        }

        public void Send(object id)
        {
            _sendObject.OnNext((id, null));
        }

        public void Send<TPayload>(int id, TPayload payload)
        {
            _sendInt.OnNext((id, payload));
        }

        public void Send<TPayload>(string id, TPayload payload)
        {
            _sendString.OnNext((id.GetHashCode(), payload));
        }

        public void Send<TPayload>(object id, TPayload payload)
        {
            _sendObject.OnNext((id, payload));
        }

        public IObservable<int> WasTriggered(int id)
        {
            return _sendInt.Where(x => x.Item1 == id).Select(x => id);
        }

        public IObservable<string> WasTriggered(string id)
        {
            var idHash = id.GetHashCode();
            return _sendString.Where(x => x.Item1.GetHashCode() == idHash).Select(x => id);
        }

        public IObservable<object> WasTriggered(object id)
        {
            return _sendObject.Where(x => x.Item1.Equals(id)).Select(x => id);
        }

        public IObservable<(int, TPayload)> WasTriggered<TPayload>(int id)
        {
            return _sendInt.Where(x => x.Item1 == id && x.Item2 is TPayload)
                .Select(x => (x.Item1, (TPayload)x.Item2));
        }

        public IObservable<(string, TPayload)> WasTriggered<TPayload>(string id)
        {
            var idHash = id.GetHashCode();
            return _sendString.Where(x => x.Item1.GetHashCode() == idHash && x.Item2 is TPayload)
                .Select(x => (id, (TPayload)x.Item2));
        }

        public IObservable<(object, TPayload)> WasTriggered<TPayload>(object id)
        {
            return _sendObject.Where(x => x.Item1.Equals(id) && x.Item2 is TPayload)
                .Select(x => (x.Item1, (TPayload)x.Item2));
        }
    }
}