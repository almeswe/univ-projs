import "dart:math";

class Note {
  int? id;
  late String contents;
  late String title;
  late String subTitle;

  Note(String title, String contents) {
    this.title = title;
    this.contents = contents;
    makeSubTitle();
  }

  Note.id(int? id, String title, String contents) {
    this.id = id;
    this.title = title;
    this.contents = contents;
    makeSubTitle();
  }

  void makeSubTitle() {
    var length = min(contents.length, 15);
    subTitle = contents.substring(0, length);
  }

  factory Note.fromMap(Map<String, dynamic> json) {
    return Note.id(json['id'], json['title'], json['contents']);
  }

  Map<String, dynamic> toMap() {
    return {'id': id, 'title': title, 'contents': contents};
  }
}
