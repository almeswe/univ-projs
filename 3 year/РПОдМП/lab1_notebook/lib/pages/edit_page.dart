import "package:flutter/material.dart";
import "package:lab1_notebook/db/notes_db.dart";
import "package:lab1_notebook/models/note.dart";

class NotebookEditPage extends StatefulWidget {
  late Note note;
  NotebookEditPage(Note note, {super.key}) {
    this.note = note;
  }

  @override
  State<NotebookEditPage> createState() => _NotebookEditPageState(note);
}

class _NotebookEditPageState extends State<NotebookEditPage> {
  late Note _note;
  late TextEditingController _titleController;
  late TextEditingController _contentsController;

  _NotebookEditPageState(Note note) {
    _note = note;
    _titleController = TextEditingController(text: _note.title);
    _contentsController = TextEditingController(text: _note.contents);
  }

  Future<void> noteSave() async {
    _note.title = _titleController.text;
    _note.contents = _contentsController.text;
    await NotesDatabase.instance.updateNote(_note);
  }

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
    return Scaffold(
      floatingActionButton: FloatingActionButton(
        foregroundColor: theme.floatingActionButtonTheme.foregroundColor,
        backgroundColor: theme.floatingActionButtonTheme.backgroundColor,
        onPressed: () async {
          await noteSave();
        },
        child: const Icon(Icons.save),
      ),
      backgroundColor: theme.colorScheme.secondary,
      body: Column(
        children: [
          Row(
            children: [
              IconButton(
                splashColor: Colors.transparent,
                highlightColor: Colors.transparent,
                hoverColor: Colors.transparent,
                iconSize: 15,
                icon: const Icon(Icons.arrow_back_ios),
                onPressed: () async {
                  await noteSave();
                  Navigator.pop(context);
                },
              ),
              Expanded(
                child: TextField(
                  controller: _titleController,
                  style: const TextStyle(
                    fontSize: 40,
                    fontFamily: "Montserrat",
                  ),
                  decoration: const InputDecoration(
                    border: InputBorder.none,
                    hintText: 'Write a title',
                  ),
                ),
              ),
              IconButton(
                onPressed: () async {
                  await NotesDatabase.instance.deleteNote(_note);
                  Navigator.pop(context);
                },
                icon: Icon(Icons.delete),
                splashColor: Colors.transparent,
                highlightColor: Colors.transparent,
                hoverColor: Colors.transparent,
                iconSize: 20,
              ),
            ],
          ),
          Expanded(
            child: Padding(
              padding: const EdgeInsets.only(left: 25.0, right: 25.0),
              child: TextField(
                maxLines: null,
                style: const TextStyle(
                  fontSize: 20,
                  fontFamily: "RobotoThin",
                ),
                controller: _contentsController,
                keyboardType: TextInputType.multiline,
                decoration: const InputDecoration(
                  border: InputBorder.none,
                  hintText: 'Write a note',
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
