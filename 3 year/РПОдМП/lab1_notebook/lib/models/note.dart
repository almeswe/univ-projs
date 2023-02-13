import "dart:math";

class Note {
  int? id;
  late String _contents;
  late String _title;
  late String _tileSubTitle;

  String get contents {
    return _contents;
  }

  String get title {
    return _title;
  }

  String get subTitle {
    return _tileSubTitle;
  }

  Note(String title, String contents) {
    _title = title;
    _contents = contents;
    makeSubTitle();
  }

  Note.id(int? id, String title, String contents) {
    id = id;
    _title = title;
    _contents = contents;
    makeSubTitle();
  }

  void makeSubTitle() {
    var length = min(_contents.length, 15);
    _tileSubTitle = _contents.substring(0, length);
  }

  factory Note.fromMap(Map<String, dynamic> json) {
    return Note.id(json['id'], json['title'], json['contents']);
  }

  Map<String, dynamic> toMap() {
    return {'id': id, 'title': title, 'contents': contents};
  }
}
