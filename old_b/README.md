# Frends.Community.JsonMapper
Json message mapper Task for FRENDS.

- [Installing](#installing)
- [Tasks](#tasks)
    - [Transform](#transform)
- [License](#license)
- [Building](#building)
- [Contributing](#contributing)

Installing
==========

You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed 'Nuget feed coming at later date'

___

Tasks
=====

## Transform

The JsonMapper task is meant for simple JSON to JSON transformation using [JUST.net](https://github.com/WorkMaze/JUST.net#just) library. 
It can also be used for JSON to XML or CSV transformation, but it is not recommeded.

Input JSON is validated before actual transformation is executed. If input is invalid or transformation fails, an exception is thrown.


### Task parameters:

| Property     | Type	    | Description    | Example        |
|:------------:|:----------:|----------------|----------------|
| Input Json | Object | Source Json to transform. Has to be String or JToken type | `{"firstName": "Jane", "lastName": "Doe" }` |
| Json Map | string | JUST transformation code. See [JUST.Net documentaion](https://github.com/WorkMaze/JUST.net#just) for details of usage | `{"fullName": "#xconcat(#valueof($.firstName), ,#valueof($.lastName))"}` |

### Task result

| Property     | Type	    | Description    | Example        |
|:------------:|:----------:|----------------|----------------|
| Result | string | Contains transformation result. | `{ "fullName" : "Jane Doe" }` |
| ToJson() | JToken | Method that returns Result string as JToken type. | |

#### Example

Simple example of combining two values from source JSON:

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
**throws exception in transformation**. This can be avoided by wrapping Input Json as follows:

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
___

License
=======
This project is licensed under the MIT License - see the LICENSE file for details

JUST Net license
--------------
WorkMaze/JUST.net is licensed under the MIT License - see the LICENSE file for details

Building
========

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.JsonMapper.git`

Restore dependencies

`nuget restore frends.community.jsonmapper`

Rebuild the project

Run Tests with nunit3. Tests can be found under

`Frends.Community.JsonMapper.Tests\bin\Release\Frends.Community.JsonMapper.Tests.dll`

Create a nuget package

`nuget pack nuspec/Frends.Community.JsonMapper.nuspec`

Contributing
============
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

Change log
==========

| Version     | Changes	   |
|:-----------:|:----------:|
| 1.0.0 | Initial version of Task |