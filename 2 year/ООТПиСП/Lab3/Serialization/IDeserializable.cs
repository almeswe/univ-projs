namespace Serialization
{
    public interface IDeserializable<T>
    {
        T DeserializeFromXML(byte[] raw);
        T DeserializeFromJSON(byte[] raw);
        T DeserializeFromText(byte[] raw);
        T DeserializeFromBinary(byte[] raw);
    }
}