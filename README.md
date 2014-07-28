CsText 

Library provides functions to handle localization. Translated string is
searched in special files with .lang extension by exact matching with original string.

Lang files are loaded in memory at program start up from current directory. 

Example:
```
[Test()]
public void ReadTest() {
   var cs = new CsText.Text("it", "locale");
   Assert.AreEqual(cs.GetText("Hello"), "ciao");
   cs.SaveFuzzyFile();
}
```

If text for translation cannot be found in specified locale name, the text will be saved and written down to a special fuzzy texts file at program shutdown. That should help to add new localization fast and without program recompilation.

# Reference

* [dlang](https://github.com/NCrashed/dtext)
* [PetaTest](http://www.toptensoftware.com/petatest/)
