namespace Luthetus.Common.RazorLib.Store.AccountCase;

public partial record AccountState
{
    public record AccountStateWithAction(Func<AccountState, AccountState> WithAction);
}