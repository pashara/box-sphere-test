namespace ProjectCore
{
    public static class EventKeys
    {
        public static readonly string HorizontalSizeChanged = "horizontal_size_changed";
        public static readonly string VerticalSizeChanged = "vertical_size_changed";
        public static readonly string BattleStartClick = "start_battle_request";
        
        
        public static readonly string ReadyForShowPrepareBattleContext = "allowed_battle_ui";
        public static readonly string DismissToShowPrepareBattleContext = "not_allowed_battle_ui";
        
        public static readonly string ReadyForShowCancelContext = "allowed_cancel_battle_ui";
        public static readonly string DismissToShowCancelContext = "not_allowed_cancle_battle_ui";
        
        public static readonly string CancelBattleClick = "cancel_battle_click";
    }
}