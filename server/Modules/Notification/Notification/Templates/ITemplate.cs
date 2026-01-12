namespace Notification.Templates;

public interface ITemplate<in TData>
{
    string GetReadyTemplate(TData data);
}