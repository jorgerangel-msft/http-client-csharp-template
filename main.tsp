import "@typespec/http";
import "@typespec/rest";

@service({
  title: "SimplePost",
})

namespace SimplePost;

using TypeSpec.Http;
using TypeSpec.Rest;

@post @route("add") op IncrementCount(@path addend: int32): int16;

op read(): {
  @statusCode _: 201 | 202 | 500;
};