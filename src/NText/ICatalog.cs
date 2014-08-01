namespace NText {
   public interface ICatalog {
       string GetText(string s);
       string Locale { get; }
       void Save();
   }
}

