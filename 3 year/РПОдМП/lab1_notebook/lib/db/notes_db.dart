import 'package:sqflite/sqflite.dart';
import 'package:lab1_notebook/models/note.dart';

class NotesDatabase {
  static final NotesDatabase instance = NotesDatabase._private();
  static Database? _database;

  Future<Database> get database async {
    return _database ??= await _initDatabase();
  }

  NotesDatabase._private();

  String getDatabasesPath() => './';

  Future<Database> _initDatabase() async {
    return await openDatabase(
      'notes.db',
      version: 1,
      onCreate: (db, version) async {
        await _onCreateDatabase(db, version);
      },
    );
  }

  Future<void> _onCreateDatabase(Database db, int version) async {
    await db.execute('''
      CREATE TABLE Notes (
        id INTEGER PRIMARY KEY, 
        title TEXT, 
        contents TEXT
      )
    ''');
  }

  Future<List<Note>> fetchNotes() async {
    var database = await instance.database;
    var notes = await database.query('notes');
    var notesList = <Note>[];
    for (var note in notes) {
      notesList.add(Note.fromMap(note));
    }
    return notesList;
  }

  void updateNote(Note note) async {}

  void insertNote(Note note) async {}

  void removeNote(Note note) async {}
}
