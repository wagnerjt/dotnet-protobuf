# Overview

Dotnet Core 2.1 Project that uses Google's Probuf message interchange format. Contains three main directories: `Cli`, `Models`, and `WebApi`.

All of the `proto` files will be located in the `Models` directory, and the resulting C# classes will be ignored via git. The WebApi has implementations on implementing the Input and Output for any requests with `Content-Type: application/x-protobuf`. The CLI is a simple project to take a binary, and perform a POST on the WebApi.

## Dependencies

The following are dependencies to run:

- DotnetCore 2.1 SDK
- Nuget or some Internal Mirror of it

### Getting Started

1. First Clone the repository:
`git clone git@github.com:wagnerjt/dotnet-protobuf.git`
2. Generate the Protocol Buffer Messages: `protoc --csharp_out=./Models/ ./Models/*.proto`
    Note: - I used Protoc 3.8 - `protoc --version`
3. Build the `CLI` or `WebApi` project `dotnet build {Project}/*.csproj`
4. Run the Project
5. Check out the `Helpful Resources` section below. Some things I wanted to test pretty quickly.

## Helpful Resources

### Protoc

Generating the Messages: `protoc --csharp_out=./Models/ ./Models/*.proto`

### Curl

Generating a Json file: `curl -H "Content-Type: application/json" https://localhost:5001/api/values/proto -k > x.json`

Getting the Proto binary: `curl -H "Content-Type: application/x-protobuf" https://localhost:5001/api/values/proto -k > x.bin`

Posting the Proto binary back: `curl -d "@x.bin" -H "Content-Type: application/x-protobuf" https://localhost:5001/api/values/person -k`

Posting the Json file back: `curl -d "@x.json" -H "Content-Type: application/json" https://localhost:5001/api/values/person -k`

### WebApi

- [Dotnet Core MVC CustomFormatters](https://stickler.de/en/information/code-snippets/httpwebrequest-with-post-data)

### CLI

- [Async CLI console apps](https://stackoverflow.com/questions/38114553/are-async-console-applications-supported-in-net-core)
- [ByteArrayContent Class](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.bytearraycontent?view=netcore-2.1)

### Protobuf

- [Dotnet Protobuf Github](https://github.com/protocolbuffers/protobuf/tree/master/csharp)

## Feedback / Contributing

Add an issue, or even create a MR. Knock out some of those TODOS down below, or something even more :).

## TODOS

- [x] - Documentation
- [x] - Create Generic Type for deserialization from web response in CLI
    - [ ] - Add Error Handling
- [ ] - Add VsCode Build Steps across the board
- [ ] - Use the Protoc's Nuget package to generate the proto files
- [ ] - Add Tests
- [ ] - Create More Complex Protos
- [ ] - Ensure ICollections of IMessages can be serialized on the Output of WebApi
- [ ] - Add Error Handling on the CLI Request
