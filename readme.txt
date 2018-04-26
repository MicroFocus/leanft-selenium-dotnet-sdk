leanft-selenium-dotnet-sdk

The LeanFT for Selenium DotNet (C#) SDK that extends the WebDriver API with additional locators and utilities. By using this SDK you can create more robust or generic identifications for your objects, and use built-in utilities rather than writing them yourself from scratch.

API:

New Locators:

- By.VisibleText

  Finds elements based on their visible text.

- By.Visible

  Finds elements based on their visibility.

- By.Role

  Finds elements based on their role.

- By.Type

  Finds elements based on their type.

- By.Attributes

  Finds elements based on their attributes (one or more). Attribute values can be defined using regular expressions.

- By.Attribute

  Finds elements based on an attribute. The attribute value can be defined using regular expressions.

- By.Styles

  Finds elements based on their computed style (one or more). Computed style values can be defined using regular expressions.

- By.Style

  Finds elements based on a computed style. The computed style value can be defined using regular expressions.

- ByAny

  Finds elements according to any of the given locators (attributes, tags, styles etc.).

- ByEach

  Finds elements based on the combination of locators (attributes, tags, styles etc.).

Regular Expression Support:

All the locators which accept a string as a value of the element's property were extended to support regular expressions, including the following Selenium native locators:

- By.Id
- By.ClassName
- By.LinkText
- By.Name
- By.TagName

Utilities:

- Utils.GetSnapshot

  Returns a snapshot (image) of the selenium element as a Base64 string.

- Utils.Highlight

  Highlights the selenium element in the browser.

- Utils.ScrollIntoView

  Scrolls the page to make the web element visible.