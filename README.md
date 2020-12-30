# Frends.Community.Json

FRENDS Community Tasks to process Json data.

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.Json/.github/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.Json/actions) ![MyGet](https://img.shields.io/myget/frends-community/v/Frends.Community.Json) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Building](#building)
- [Contributing](#contributing)
- [Tasks](#Tasks)
     - [EnforceJsonTypes](#EnforceJsonTypes)
     - [JsonMapper](#JsonMapper)
- [Change Log](#change-log)

## Installing

You can install the task via FRENDS UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.Json

## Building

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.Json.git`

Rebuild the project

`dotnet build`

Run Tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

## Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

## Tasks

### EnforceJsonTypes

Frends task for enforcing types in Json documents. The main use case is when you e.g. convert XML into Json and you lose all the type info in the resulting Json document. With this task you can restore the types inside the Json document.

#### Input Properties

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| Json | `string` | Json document to process | `{ "prop1": "123", "prop2": "true" }`
| Rules | `Array[JsonTypeRule]` | Rules for enforcing | `[`<br/>`{ "$.prop1", Number },`<br/>`{ "$.prop2", Boolean }`<br/>`]` |

##### JsonTypeRule

| Property             | Type                 | Description                          | Example |
| ---------------------| ---------------------| ------------------------------------ | ----- |
| Json path | `string` | Json path for the rule to use | `$.prop1` |
| Data type | Enum<> | Data type to enforce | String |


#### Returns

The result is a Json string with types converted. Given the following input:

Json:  `{ "prop1": "123", "prop2": "true" }`

Rules:
- `"$.prop1" => Number`
- `"$.prop2" => Boolean`

The output would be: `{ "prop1": 123.0, "prop2": true }`

### JsonMapper

The JsonMapper task is meant for simple Json to Json transformation using [JUST.net](https://github.com/WorkMaze/JUST.net) library. 
It can also be used for Json to XML or CSV transformation, but it is not recommeded.

Input Json is validated before the actual transformation is executed. If the input is invalid or transformation fails, an exception is thrown.

#### Input Properties

| Property     | Type	    | Description    | Example        |
|:------------:|:----------:|----------------|----------------|
| Input json | `string` or `JToken` | Source Json to transform. Has to be String or JToken type. | `{"firstName": "Jane", "lastName": "Doe" }` |
| Json map | `string` | JUST transformation code. See [JUST.Net documentaion](https://github.com/WorkMaze/JUST.net#just) for details of usage | `{"fullName": "#xconcat(#valueof($.firstName), ,#valueof($.lastName))"}` |

#### Returns

The result is an object with following properties

| Property     | Type	    | Description    | Example        |
|:------------:|:----------:|----------------|----------------|
| Result | `string` | Contains transformation result. | `{ "fullName" : "Jane Doe" }` |
| ToJson() | `JToken` | Method that returns Result string as JToken type. | |

#### Example

Simple example of combining two values from source Json:

**Input Json:**
```json
{
  "firstName": "John",
  "lastName": "Doe"
}
```
**Json Map:**
```json
{
  "Name": "#xconcat(#valueof($.firstName), ,#valueof($.lastName))"
}
```

**Transformation result:**
```json
{
  "Name": "John Doe"
}
```

#### Known issues

Json which root node is of type array does not work. It has to wrapped around an object.
Example:

Input
```json
[{
  "Name": "John Doe"
},
{
  "Name": "John Doe"
}]
```
with Json Map
```json
{
  "Name": "#valueof($.[0].firstName)"
}
```
throws exception in transformation. **This can be avoided by wrapping Input Json as follows:**

```json
{ "root":
[{
  "Name": "John Doe"
},
{
  "Name": "John Doe"
}]
}
```

## Change Log

| Version | Changes |
| ------- | ------- |
| 1.0.0   | Initial implementation. Two different tasks combined to one repo. |
