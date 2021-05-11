unit DB;

interface

uses SysUtils, List, Defines;

function SaveToTextFile(path : string) : boolean;
function SaveToTypeFile(path : string; data : TCustomList) : boolean;

function LoadFromTextFile(path : string) : TEmployees;
function LoadFromTypeFile(path : string) : TCustomList;

implementation

var TypeFile : file of TEmployee;

function ConvertCharFlowToType(flow : TStringArray) : TEmployees;
begin

end;
function SaveToTextFile(path : string) : boolean;
begin

end;
function SaveToTypeFile(path : string; data : TCustomList) : boolean;
var i : integer;
var gotted : TEmployee;
begin
  if FileExists(path) then begin
    AssignFile(TypeFile, path);
    Rewrite(TypeFile);
    for i := 0 to data.size()-1 do begin
      gotted := data.get(i);
      Write(TypeFile, gotted);
    end;
    CloseFile(TypeFile);
    exit(true);
  end
  else
    exit(false);
end;
function LoadFromTextFile(path : string) : TEmployees;
begin

end;
function LoadFromTypeFile(path : string) : TCustomList;
var i : integer;
var data : TCustomList;
var readed : TEmployee;
begin
  data.init();
  if FileExists(path) then begin
    AssignFile(TypeFile, path);
    Reset(TypeFile);
    while not eof(TypeFile) do begin
      Read(TypeFile, readed);
      data.append(readed);
    end;
    CloseFile(TypeFile);
  end;
  exit(data);
end;

end.
