using SCG = System.Collections.Generic;
using RE = System.Text.RegularExpressions;
namespace NText {
   public class PoCatalog: ICatalog {
      private SCG.Dictionary<string, string> map = new SCG.Dictionary<string, string>();
      public PoCatalog(string locale) {
         if (string.IsNullOrEmpty(locale)) {
            throw new System.ArgumentNullException(locale);
         }
         this.Locale = locale;
      }
      public string Locale { get; private set; }

      /**
        Add Catalog messages from a .po file.
        Doesn't initialize the collection, so it can be called from
        a loop if needed.
        */
      public void LoadFromFile(string filepath) {
         LoadFromReader(new System.IO.StreamReader(filepath, System.Text.Encoding.UTF8, true));
      }

      /**
        Add Catalog messages from the specified TextReader
        (which hopefully points at a .po file)

        Doesn't initialize the collection, so it can be called from
        a loop if needed.
        */
      public void LoadFromReader(System.IO.TextReader reader) {
         int state = UNKNOWN;
         string line;

         string msgId = string.Empty;
         string msgStr = string.Empty;
         while ((line = reader.ReadLine()) != null) {
            state = GetNewStateFromLine(state, line);
            switch (state) {
               default:
               case UNKNOWN:
                  break;
               case ID:
                  msgId += GetToken(line);
                  break;
               case STR:
                  msgStr += GetToken(line);
                  break;
               case MULTI:
                  msgStr += System.Environment.NewLine;
                  msgStr += GetToken(line);
                  break;
               case END:
                  AddUniq(msgId, msgStr);
                  msgId = string.Empty;
                  msgStr = string.Empty;
                  break;
            }
         }
         AddUniq(msgId, msgStr);
         reader.Close();
      }

      private const int UNKNOWN = 0;
      private const int ID = 1;
      private const int STR = 2;
      private const int MULTI = 3;
      private const int END = 4;
      private int GetNewStateFromLine(int previousState, string line) {
         int state = previousState;
         switch (previousState) {
            case UNKNOWN:
            case END:
               if (StartsWithMsgId(line)) {
                  state = ID;
               }
               break;
            case ID:
               if (string.IsNullOrEmpty(line)) {
                  state = END;
               } else if (StartsWithMsgStr(line)) {
                  state = STR;
               }
               break;
            case STR:
               if (string.IsNullOrEmpty(line)) {
                  state = END;
               } else if (!StartsWithMsgStr(line)) {
                  state = MULTI;
               }
               break;
            case MULTI:
               if (string.IsNullOrEmpty(line)) {
                  state = END;
               }
               break;
         }
         return state;
      }

      private bool StartsWithMsgId(string line) {
         return !string.IsNullOrEmpty(line) && line.StartsWith("msgid");
      }

      private bool StartsWithMsgStr(string line) {
         return !string.IsNullOrEmpty(line) && line.StartsWith("msgstr");
      }

      private readonly RE.Regex textMatcher = new RE.Regex(@"""(.*)""", RE.RegexOptions.Compiled);
      private string GetToken(string line) {
         string token = string.Empty;
         RE.Match match = textMatcher.Match(line);
         if (match.Success) {
            token = @match.Groups[1].Value;
         }
         return token;
      }

      private void AddUniq(string msgId, string msgStr) {
         if (!string.IsNullOrEmpty(msgId)) {
            if (!map.ContainsKey(msgId)) {
               map[msgId] = msgStr;
            }
         }
      }

      public string GetText(string s) {
         string translated = s;
         if (!string.IsNullOrEmpty(s)) {
            if (map.ContainsKey(s) && !string.IsNullOrEmpty(map[s])) {
               translated = map[s];
            }
         }
         return translated;
      }

      public void Save() {
         throw new System.NotImplementedException("Not implemented.");
      }
   }
}
