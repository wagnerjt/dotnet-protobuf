# Overview

Blah

## Dependencies

The following are dependencies to run.

## Helpful Commands

Using curl

Generating the Protocs
`protoc --csharp_out=./models/ ./models/*.proto`

Getting the Proto binary. `curl -H "Content-Type: application/x-protobuf" https://localhost:5001/api/values/proto -k > x.bin`
Posting the Proto binary back `curl -d "@x.bin" -H "Content-Type: application/x-protobuf" https://localhost:5001/api/values/person -k`
Posting the Json file back. `curl -d "@x.json" -H "Content-Type: application/json" https://localhost:5001/api/values/person -k`

## WebApi

[Dotnet Core MVC CustomFormatters](https://stickler.de/en/information/code-snippets/httpwebrequest-with-post-data)

## CLI

* [Async CLI console apps](https://stackoverflow.com/questions/38114553/are-async-console-applications-supported-in-net-core)
* [ByteArrayContent Class](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.bytearraycontent?view=netcore-2.1)
