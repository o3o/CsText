using SCG = System.Collections.Generic;
using System.IO;
namespace CsText {
   public class Text {
      public Text(string locale): this(locale, "../locale") {}

      public Text(string locale, string localeDir) {
         if (string.IsNullOrEmpty(locale)) {
            throw new System.ArgumentNullException(locale);
         }
         this.Locale = locale;
         if (string.IsNullOrEmpty(localeDir)) {
            throw new System.ArgumentNullException(localeDir);
         }
         this.localeDir = localeDir;
         string filename = GetLocaleFileName(locale);
         map = ReadFile(filename);
      }


      public string Locale { get; private set; }

      private string localeDir = "../locale";
      public string LocaleDirectory {
         get { return localeDir; }
      }

      private string GetLocaleFileName(string locale) {
         return System.IO.Path.Combine(this.LocaleDirectory, locale) + ".lang";
      }

      private SCG.Dictionary<string, string> ReadFile(string file) {
         return Read(new StreamReader(file, System.Text.Encoding.UTF8));
      }

      private SCG.Dictionary<string,string> Read(TextReader reader) {
         var dic = new SCG.Dictionary<string, string>();
         string line;
         while ((line = reader.ReadLine()) != null) {
            if (IsValidLine(line)) {
               string[] tokens = line.Split('=');
               if (IsValidToken(tokens)) {
                  string k = Clean(tokens[0]);
                  string v = Clean(tokens[1]);
                  dic[k] = v;
               }
            }
         }
         return dic;
      }

      private bool IsValidLine(string line) {
         return !string.IsNullOrEmpty(line) && !line.StartsWith("#");
      }

      private bool IsValidToken(string[] tokens) {
         return tokens != null
            && tokens.Length > 1
            && !string.IsNullOrEmpty(tokens[0])
            && !string.IsNullOrEmpty(tokens[1]);
      }

      private string Clean(string v) {
         return v.Trim().Trim('"');
      }

      private SCG.Dictionary<string, string> map = new SCG.Dictionary<string, string>();
      private SCG.Dictionary<string, string> fuzzyMap = new SCG.Dictionary<string, string>();

      public string GetText(string s) {
         if (map.ContainsKey(s)) {
            return map[s];
         }

         if (!fuzzyMap.ContainsKey(s)) {
            fuzzyMap[s] = string.Empty;
         }
         return s;
      }

      public void SaveFuzzyFile() {
         WriteFuzzyFile(GetFuzzyLocaleFileName(this.Locale));
      }

      private string GetFuzzyLocaleFileName(string locale) {
         return System.IO.Path.Combine(this.LocaleDirectory, locale) + ".fuzzy";
      }

      private void WriteFuzzyFile(string file) {
         Write(file, fuzzyMap);
      }

      private void Write(string fileName, SCG.Dictionary<string, string> dic) {
         using (StreamWriter writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8)) {
            foreach (var entry in dic) {
               string line = string.Format("\"{0}\"=\"\"", entry.Key);
               writer.WriteLine(line);
            }
         }
      }
   }
}
