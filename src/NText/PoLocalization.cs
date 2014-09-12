using SCG = System.Collections.Generic;
namespace NText {
   public class PoLocalization: ILocalization {
      private readonly SCG.IDictionary<string, ICatalog> catalogs;
      public PoLocalization() {
         catalogs = new SCG.Dictionary<string, ICatalog>();
      }

      public void LoadFromDirectory(string basePathAbsolute) {
         if (System.IO.Directory.Exists(basePathAbsolute)) {
            foreach (string dir in System.IO.Directory.GetDirectories(basePathAbsolute)) {
               string locale = GetLanguageCode(dir);
               var catalog = new PoCatalog(locale);
               foreach (string filename in System.IO.Directory.GetFiles(dir, "*.po")) {
                  catalog.LoadFromFile(filename);
               }
               catalogs[locale] = catalog;
            }
         }
      }

      private string GetLanguageCode(string dir) {
         string dirName = System.IO.Path.GetDirectoryName(dir) + System.IO.Path.DirectorySeparatorChar;
         return dir.Replace(dirName, string.Empty).ToLower();
      }

      public string GetText(string msgID, string locale) {
         return this.GetCatalog(locale).GetText(msgID);
      }

      public ICatalog GetCatalog(string languageCode) {
         if (!catalogs.ContainsKey(languageCode.ToLower())) {
            return new NullCatalog();
         } else {
            return catalogs[languageCode.ToLower()];
         }
      }
   }
}
