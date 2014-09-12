namespace NText {
   public interface ILocalization {
      ICatalog GetCatalog(string locale);
      string GetText(string msgID, string locale);
      void LoadFromDirectory(string basePathAbsolute);
   }
}
