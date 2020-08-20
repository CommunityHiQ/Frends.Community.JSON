# Frends.Community.JSON.EnforceJSONTypes
Frends task for enforcing types in JSON documents. The main use case is when you e.g. convert XML into JSON and you lose all the type info in the resulting JSON document. With this task you can restore the types inside the JSON document.

- [Installing](#installing)
- [Tasks](#tasks)
  - [CreateJwtToken](#CreateJwtToken)
- [License](#license)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing
You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
'Nuget feed coming at later date'

Tasks
=====

## EnforceJsonTypes

### Parameters

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| Json | string | JSON document to process | `{ "prop1": "123", "prop2": "true" }`
| Rules | JsonTypeRule[] | Rules for enforcing | `[`<br/>`{ "$.prop1", Number },`<br/>`{ "$.prop2", Boolean }`<br/>`]` |

### Result
Result contains the JSON document with types converted. Given the following input:

JSON:  `{ "prop1": "123", "prop2": "true" }`

Rules:
- `"$.prop1" => Number`
- `"$.prop2" => Boolean`

The output would be: `{ "prop1": 123.0, "prop2": true }`

# License

This project is licensed under the MIT License - see the LICENSE file for details

# Building

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.JSON.EnforceJSONTypes`

Restore dependencies

`nuget restore`

Rebuild the project

Run Tests with MSTest. Tests can be found under

`Frends.Community.JSON.EnforceJSONTypes.Tests\bin\Release\Frends.Community.JSON.EnforceJSONTypes.Tests.dll`

Create a nuget package

`nuget pack Frends.Community.JSON.EnforceJSONTypes.nuspec`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version             | Changes                 |
| ---------------------| ---------------------|
| 1.0.2 | Initial version of EnforceJSONTypes task |
| 1.0.3 | Fix issue with converting JTokens into JArrays |
| 1.0.4 | Fixing issue with double arrays produces when forcing array types, basic int data type recognition in serialization step added |
