# ScteThreeFiver: A C# SCTE-35 Decoder

`ScteThreeFiver` is a c# library to supports decorating binary Digital Program Insertion Cueing Messages.

I was unable to find a suitable SCTE-35 library for C# and needed one for an existing C# project. Note that C# 
is not my main language and so this may be very poorly written.

The primary reference is the SCTE website documentation https://account.scte.org/standards/library/catalog/scte-35-digital-program-insertion-cueing-message/

The library is very heavily inspired by the excellent ComCast Go https://github.com/Comcast/scte35-go and
Futzu Python ThreeFive https://github.com/futzu/threefive libraries - with code taken from both.

Validation is limited to only the SCTE Commands we use at work and those listed in the SCTE-35 Specification Examples 
(one of which I cannot get to decode properly, so be warned, there are issues and completely untested sections). I used both
https://tools.middleman.tv/scte35-parser and https://iodisco.com/cgi-bin/scte35parser as decode references.

I have tried to copy from the Specification as accurately as I can but C# is not my main language.

There are a couple of "company specific" extension properties to the SCTE spec, these should not impact using this for other things.

## Limitations
I have only been able to test on the SCTE commands we use. I did use the SCTE examples but 1 of them (test 1) seems to not work due to exceeding the length of the data
Additionally, I started using a slightly older doc before changing to the latest so there may be some missing fields.

Does not support Encryption of the message

## Usage
The class can read in either a byte array or a Base64 string (e.g. from the DASH Manifest EventStream)

```
string base64_1 = "/DAlAAAAAA4QAP/wFAUwLMaof+//z0uXAP4AUmXAwlQBAQAAm2zCrQ==";
Scte35 res = Scte35.DecoderBase64(base64_1);
Console.WriteLine("SCTE Command Type = " + res.SpliceInfoSection.SpliceCommand.GetType().ToString());

```
All the useful values are within the SpliceInfoSection class

## Updates
Please feel free to correct, improve, this was just to help me with a project for reading Manifest SCTE markers so there's no guarentee it will do anything vaguely right!







