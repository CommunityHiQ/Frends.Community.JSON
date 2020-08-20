# Frends.Community.JSON

FRENDS Community Tasks to process JSON.

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.JSON/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.JSON/actions) ![MyGet](https://img.shields.io/myget/frends-community/v/Frends.Community.JSON) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [EnforceJsonTypes](#EnforceJsonTypes)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the task via FRENDS UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.JSON

# Tasks

## EnforceJsonTypes

Frends task for enforcing types in JSON documents. The main use case is when you e.g. convert XML into JSON and you lose all the type info in the resulting JSON document. With this task you can restore the types inside the JSON document.

### Properties

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| Json | string | JSON document to process | `{ "prop1": "123", "prop2": "true" }`
| Rules | JsonTypeRule[] | Rules for enforcing | `[`<br/>`{ "$.prop1", Number },`<br/>`{ "$.prop2", Boolean }`<br/>`]` |

### Returns

Result contains the JSON document with types converted. Given the following input:

JSON:  `{ "prop1": "123", "prop2": "true" }`

Rules:
- `"$.prop1" => Number`
- `"$.prop2" => Boolean`

The output would be: `{ "prop1": 123.0, "prop2": true }`

# Building

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.JSON.git`

Rebuild the project

`dotnet build`

Run Tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ------- | ------- |
| 0.0.1   | Development stil going on. |
