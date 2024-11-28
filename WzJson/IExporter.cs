namespace WzJson;

public interface IExporter
{
    public bool Supports(IData data);
    public void Export(IData data);
}