Chimp
=====
### A gimpish C# library for basic image alterations

* Resize
* Constrained Scale
* Crop
* Saving
* Delete

**Constrained Scale**: *scales the image proportionally within the defined dimensions*



1. Add one or more DirectoryConfig implementations for the directory locations.
  * LargeImageDirectory : *"path/images/large"*
  * ThumbnailImageDirectory : *"path/images/thumbnails"*

2. Use the default implementations of the dependencies or roll your own implementations.