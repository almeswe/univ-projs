unit DB;

interface

uses SysUtils, List, Defines;

function SaveToTextFile(path : string; data : TCustomList) : boolean;
function SaveToTypeFile(path : string; data : TCustomList) : boolean;

function LoadFromTextFile(path : string) : TCustomList;
function LoadFromTypeFile(path : string) : TCustomList;

implementation

var TextTypeFile : TextFile;
var TypeFile : file of TEmployee;

function SaveToTextFile(path : string; data : TCustomList) : boolean;
var i : integer;
var gotted : TEmployee;
begin
  if FileExists(path) then begin
    AssignFile(TextTypeFile, path);
    Rewrite(TextTypeFile);
    for i := 0 to data.Size()-1 do begin
      gotted := data.GetData(i);
      Writeln(TextTypeFile, gotted.Name);
      Writeln(TextTypeFile, gotted.Surname);
      Writeln(TextTypeFile, gotted.Midname);
      Writeln(TextTypeFile, gotted.Project.Name);
      Writeln(TextTypeFile, gotted.Project.Task);
      Writeln(TextTypeFile, gotted.Project.Deadline);
      Writeln(TextTypeFile, gotted.Shedule.Start);
      Writeln(TextTypeFile, gotted.Shedule.Finish);
    end;
    CloseFile(TextTypeFile);
    exit(true);
  end
  else
    exit(false);
end;


function SaveToTypeFile(path : string; data : TCustomList) : boolean;
var i : integer;
var gotted : TEmployee;
begin
  if FileExists(path) then begin
    AssignFile(TypeFile, path);
    Rewrite(TypeFile);
    for i := 0 to data.Size()-1 do begin
      gotted := data.GetData(i);
      Write(TypeFile, gotted);
    end;
    CloseFile(TypeFile);
    exit(true);
  end
  else
    exit(false);
end;

function LoadFromTextFile(path : string) : TCustomList;
var i : integer;
var data : TCustomList;
var readed : TEmployee;
begin
  data.Init();
  if FileExists(path) then begin
    AssignFile(TextTypeFile, path);
    Reset(TextTypeFile);
    while not eof(TextTypeFile) do begin
      Readln(TextTypeFile, readed.Name);
      Readln(TextTypeFile, readed.Surname);
      Readln(TextTypeFile, readed.Midname);
      Readln(TextTypeFile, readed.Project.Name);
      Readln(TextTypeFile, readed.Project.Task);
      Readln(TextTypeFile, readed.Project.Deadline);
      Readln(TextTypeFile, readed.Shedule.Start);
      Readln(TextTypeFile, readed.Shedule.Finish);
      data.Append(readed);
    end;
    CloseFile(TextTypeFile);
  end;
  exit(data);
end;

function LoadFromTypeFile(path : string) : TCustomList;
var i : integer;
var data : TCustomList;
var readed : TEmployee;
begin
  data.Init();
  if FileExists(path) then begin
    AssignFile(TypeFile, path);
    Reset(TypeFile);
    while not eof(TypeFile) do begin
      Read(TypeFile, readed);
      data.Append(readed);
    end;
    CloseFile(TypeFile);
  end;
  exit(data);
end;

end.
