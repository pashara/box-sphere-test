using System;
using TMPro;
using UniRx;

namespace ThirdParty.TextMeshPro.UniRxExtensions
{
    public static class TextMeshProRxExtensions
    {
        public static IObservable<string> OnValueChangedAsObservable(this TMP_InputField inputField)
        {
            // return inputField.onValueChanged.AsObservable();
            return Observable.CreateWithState<string, TMP_InputField>(inputField, (i, observer) =>
            {
                observer.OnNext(i.text);
                return i.onValueChanged.AsObservable().Subscribe(observer);
            });
        }
    }
}