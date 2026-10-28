# 06: What A Record And An Email Carry

Epic 01. Size: small. Changes: `source\Roy.Logging\Roy.Logging\`
(`Domain\Attributes\ExceptionDetail`, `Extensions\EmailExtensions`,
`Extensions\DTO\MessageDetailDTOExtensions`).

## Status

Not started. Waits for decision 2 and for 01.

## Blocked By

Decision 2.

## Depends On

01.

## Goal

An exception record at any level says where the exception was thrown. Each email's subject
carries its own issue's Id and level. The DTO an API receives carries the caller's parameters.

## What Happens Today

See the epic's *What a record and an email carry*.

## Scope

- `ExceptionDetail.SetExceptionTrace`: `this.StackTrace = exception.StackTrace ?? string.Empty`
  on every call; `this.ExceptionTrace = exception` stays behind `Trace` and `Debug`. The
  constructor's comment ("If level is Trace or Debug, it will add the full exception") and the
  two `Level` comments stay true and are not touched; the `StackTrace` property's comment says
  it is always set. The file record, the database record (`ExceptionRecord.ExceptionStackTrace`)
  and the email (`Tag.StackTrace`) all read the same property, so they follow.
- `EmailExtensions.SetDefaultValues`: the subject's tags are replaced on a copy for this
  message, never written back to the setting. The template is `setting.DefaultEmailSubject` if
  George set one, else the resource's; the result is the subject `ToMimeMessage` uses
  (`receiver.Subject` still wins when set, as today). `DefaultEmailBody` is already handled
  that way by `ToMessageBody` and is not touched.
- `MessageDetailDTOExtensions.ToDTO(this Method)`: `Parameters = method.Parameters.ToDTO()`.

## Not In This Feature

- What `Method.LoadParametersInformation` puts in those parameters (out of scope in the epic).
- The HTML templates, the labels, the four languages.

## Tests

- An `Error`-level save of an exception thrown in a named helper method: the record's
  `StackTrace` contains that method's name; `ExceptionTrace` is absent. A `Debug`-level save:
  both present. The first fails before and passes after.
- The subject: `ToMimeMessage` called twice on one `EmailSetting` for two issues with different
  Ids: each subject contains its own Id, and `setting.DefaultEmailSubject` is as it was
  before the first call. Fails before (the second subject has the first Id).
- `ToDTO` of a `Method` with two parameters has two `Parameters`.

## As Built

Not built.
