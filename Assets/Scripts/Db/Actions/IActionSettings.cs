namespace Db.Actions
{
    public interface IActionSettings
    {
        IActionBase GetAction(EActionType actionType);
    }
}