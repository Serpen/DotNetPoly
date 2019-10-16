namespace DotNetPoly
{
    public enum eActionType
    {
        Random = -1,
        None,
        Pass,
        WaitingForTurn,
        EndTurn,
        LoseTurn,
        DiceOnly,
        Move,
        BuyHouse,
        // FieldInfo

        // EscapePrison
        PayRent,
        JumpToJail,
        GoToField,
        JumpToField,
        JumpToRelativField,
        GiveUp,
        PayToStack,
        CollectStack,
        CashToBank,
        CashFromBank,
        CashFromAll,
        CashToPlayer,
        CashFromPlayer,
        UpgradeHouse,
        AddMove,
        PayPerHouse,
        Card_GetFreeParking,
        Card_UseFreeParking,
        Card_GetFreeJail,
        Card_UseFreeJail,
        CreditForHouse,
        CreditReleaseHouse,
        IncreaseFixedDeposite,
        TerminateFixedDeposite,
        Auction,
        Trade
    }
}
