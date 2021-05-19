unit Defines;

interface

type int = integer;
type uint = uint32;
type bool = boolean;

type TIntArray = array of int;

type TGenKind = (RandomArray, SortedArray, ReversedArray);

type TSort = function(data : TIntArray; out comps,perms : uint) : TIntArray;

function GenKindToString(kind : TGenKind) : string;

implementation

function GenKindToString(kind : TGenKind) : string;
begin
  case kind of
     TGenKind.RandomArray :   exit('Random');
     TGenKind.SortedArray :   exit('Sorted');
     TGenKind.ReversedArray : exit('Reversed');
  end;
end;

end.
