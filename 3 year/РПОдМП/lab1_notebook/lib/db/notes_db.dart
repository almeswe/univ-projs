import 'package:lab1_notebook/models/note.dart';

import 'package:sqflite/sqflite.dart';

import 'package:sqflite_common/sqlite_api.dart';
import 'package:sqflite_common_ffi/sqflite_ffi.dart';

class NotesDatabase {
  static final NotesDatabase instance = NotesDatabase._private();
  static Database? _database;

  Future<Database> get database async {
    return _database ??= await _initDatabase();
  }

  NotesDatabase._private();

  Future<Database> _initDatabase() async {
    sqfliteFfiInit();
    var databaseFactory = databaseFactoryFfi;
    var path = '/home/almeswe/Documents/notes.db';
    return await databaseFactory.openDatabase(
      path,
      options: OpenDatabaseOptions(
        version: 1,
        onCreate: (db, version) async {
          await _onCreateDatabase(db, version);
        },
      ),
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

  Future<List<Note>> fetchNotes(String? filter) async {
    var database = await instance.database;
    var notes = await database.query('Notes');
    var notesList = <Note>[];
    for (var note in notes) {
      var instance = Note.fromMap(note);
      if (filter != null && instance.title.contains(filter)) {
        notesList.add(instance);
      }
    }
    return notesList;
  }

  Future<int> insertNote(Note note) async {
    var database = await instance.database;
    var id = await database.insert('Notes', note.toMap());
    note.id = id;
    return id;
  }

  Future<int> updateNote(Note note) async {
    var database = await instance.database;
    return await database.update(
      'Notes',
      note.toMap(),
      where: 'id = ?',
      whereArgs: [note.id],
    );
  }

  Future<int> deleteNote(Note note) async {
    var database = await instance.database;
    return await database.delete(
      'Notes',
      where: 'id = ?',
      whereArgs: [note.id],
    );
  }
}
