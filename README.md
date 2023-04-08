# Simple tools - Custom SO Icons
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## About
**Custom SO Icons** - simple editor addon that allows to show custom scriptable object icons in the Project view

<p align="center">
  <img width="600" src="https://user-images.githubusercontent.com/44195161/230739721-1632e02a-9d62-4215-99c1-0ab3c5e965ab.png">
</p>

## Overview

This addon speeds up the interaction with the editor by x100 times - you no longer have to look at the names when you look for an object, because you can simply display the icon directly on the asset.

<p align="center">
  <img width="700" src="https://user-images.githubusercontent.com/44195161/230738980-ad7ddc8b-8333-44c6-b631-236b5d67834c.png">
</p>

You can change scale of icon, and set background if needed.

<p align="center">
  <img width="500" src="https://user-images.githubusercontent.com/44195161/230740201-3a76b449-3ce1-4eb0-adc3-b08712612b84.png">
</p>


*Also, if you have the [**Odin Inspector**](https://odininspector.com/) installed in your project, the icons will be displayed in any other fields of the inspector view. No additional settings are needed.*
<p align="center">
  <img width="500" src="https://user-images.githubusercontent.com/44195161/230739281-c200e1f6-b760-4b70-a51e-edf65318863e.png">
</p>


## Minimum Requirements
* Unity 2020 and above

*Not necessary:*
* [**Odin Inspector**](https://odininspector.com/) - only for display SO icons in inspector fields

### Install via GIT URL

Go to ```Package Manager``` -> ```Add package from GIT url...``` -> Enter ```https://github.com/fffogil/asset-icons.git``` -> Click ```Add```

You will need to have Git installed and available in your system's PATH.

## Usage

You have 3 ways to declare which icon to use for display.

**Fisrst way** - Simply mark any ```Sprite``` field or property in your SO class as ```[CustomAssetIcon]```

<p align="center">
  <img width="600" src="https://user-images.githubusercontent.com/44195161/230740607-9fef8fbd-f150-4669-9329-5642ea93fb22.png">
</p>

**Second way** - If you want to assign icon to read-only class that you cannot change, you can create another class anywhere inside your project, inherit it from the ```ICustomEditorIconDeclarator```, and get an icon.

<p align="center">
  <img width="600" src="https://user-images.githubusercontent.com/44195161/230740765-a4448b78-c925-4d93-abc4-3f0e946e3ba6.png">
</p>

**Third way** - For classes that you just want to assign an icon that won't change depending on the content, you can add override option in addon settings, which are in ```Assets/Resources```

<p align="center">
  <img width="600" src="https://user-images.githubusercontent.com/44195161/230741154-54a17fe5-9b6c-4ad1-93b9-080c150c0bac.png">
</p>

*That's all! I'd appreciate it if you'd leave a star on the github.*

## License

MIT License

Copyright (c) 2023 Ligofff

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
