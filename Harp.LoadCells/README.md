## Harp LoadCells

This repository contains the device metadata, firmware and high-level interface to the LoadCells device. Below are simple getting started instructions for maintainers to create a new device using the automatic code generators.

### Device metadata

#### Prerequisites

1. Install [Visual Studio Code](https://code.visualstudio.com/)
2. Install the [YAML](https://marketplace.visualstudio.com/items?itemName=redhat.vscode-yaml) extension.

The `device.yml` file in the root of the project contains the LoadCells device metadata. A complete specification of device registers, including any bit masks, group masks, and payload specs needs to be provided for code generation to work.

### Generating interface and firmware

#### Prerequisites

1. Install [`dotnet`](https://dotnet.microsoft.com/)
2. Install `dotnet-t4`
```
dotnet tool install -g dotnet-t4
```

The `Generators` folder contains all text templates and project files required to generate both the firmware headers and the interface for the LoadCells device. To run the text templating engine just build the project inside this folder.

```
cd Generators
dotnet build
```
