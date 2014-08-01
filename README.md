# NText 

Library provides functions to handle localization. Translated string is
searched in special files with `.lang` extension by exact matching with original string.

Lang files are loaded in memory at program start up from current directory. 


## Example

```
[Test()]
public void load_from_dcatalog() {
    // This will load translations from "./tests/locale/it.lang"
   var catalog = new NText.DCatalog("it", "tests/locale");
   Assert.AreEqual(catalog.GetText("Hello"), "ciao");

   // Save on ./tests/locale/it.fuzzy
   catalog.Save();
}

public void load_from_default_locale_direcory() {
    // This will load translations from "../locale/it.lang"
   var catalog = new NText.DCatalog("it");
   Assert.AreEqual(catalog.GetText("Hello"), "ciao");

   // Save on ../locale/it.fuzzy
   catalog.Save();
}
```

If text for translation cannot be found in specified locale name, the text will be saved and written down to a special fuzzy texts file when SaveFuzzyFile is called. That should help to add new localization fast and without program recompilation.

## Compile
```
$ make
```

## Compile and test
```
$ make test
```

## References

* [dlang](https://github.com/NCrashed/dtext)
* [PetaTest](http://www.toptensoftware.com/petatest/)
