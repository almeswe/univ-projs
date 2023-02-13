import "package:flutter/material.dart";
import 'package:flutter/scheduler.dart';
import "package:lab1_notebook/db/notes_db.dart";
import "package:lab1_notebook/models/note.dart";
import "package:lab1_notebook/pages/edit_page.dart";

class NotebookHomePage extends StatefulWidget {
  const NotebookHomePage({super.key});

  @override
  State<NotebookHomePage> createState() => _NotebookHomePageState();
}

class _NotebookHomePageState extends State<NotebookHomePage> {
  var notes = <Note>[];
  var dummy = 0;
  var noteTitleStyle = const TextStyle(
    fontSize: 25,
    fontFamily: 'RobotoBold',
  );
  var noteSubTitleStyle = const TextStyle(
    fontSize: 13,
    fontFamily: 'RobotoThin',
    color: Color(0xFFD3D3D3),
  );
  var _searchController = TextEditingController();

  @override
  void initState() {
    super.initState();
  }

  Widget noteBuilder(Note note) {
    var theme = Theme.of(context);
    return Padding(
      padding: const EdgeInsets.only(left: 15.0, right: 15.0, bottom: 25.0),
      child: ListTile(
        tileColor: theme.colorScheme.background,
        contentPadding: const EdgeInsets.only(left: 24.0),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(5.0),
        ),
        title: Text(
          note.title,
          style: noteTitleStyle,
        ),
        subtitle: Text(
          note.subTitle,
          style: noteSubTitleStyle,
        ),
        onTap: () {
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) {
                return NotebookEditPage(note);
              },
            ),
          );
        },
      ),
    );
  }

  Future<void> noteNew() async {
    var date = DateTime.now();
    var dateStr = "${date.day}-${date.month}-${date.year}";
    var note = Note(dateStr, "New Note");
    await NotesDatabase.instance.insertNote(note);
    setState(() {
      notes.add(note);
    });
  }

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
    SchedulerBinding.instance.addPostFrameCallback(
      (_) => setState(() {}),
    );
    return Scaffold(
      backgroundColor: theme.colorScheme.secondary,
      appBar: AppBar(
        title: const Text('Notes'),
        titleSpacing: 25.0,
        titleTextStyle: const TextStyle(
          fontSize: 40,
          fontFamily: "Montserrat",
        ),
        foregroundColor: theme.appBarTheme.foregroundColor,
        backgroundColor: theme.appBarTheme.backgroundColor,
      ),
      floatingActionButton: FloatingActionButton(
        foregroundColor: theme.floatingActionButtonTheme.foregroundColor,
        backgroundColor: theme.floatingActionButtonTheme.backgroundColor,
        onPressed: () async {
          await noteNew();
        },
        child: const Icon(Icons.add),
      ),
      body: Column(
        mainAxisSize: MainAxisSize.max,
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SizedBox(height: 15),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 15.0),
            child: TextField(
              controller: _searchController,
              decoration: InputDecoration(
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(5),
                ),
              ),
            ),
          ),
          Expanded(
            child: Padding(
              padding: const EdgeInsets.only(top: 15.0),
              child: FutureBuilder(
                future:
                    NotesDatabase.instance.fetchNotes(_searchController.text),
                builder: (context, snapshot) {
                  if (!snapshot.hasData) {
                    return const Center(child: Text('Write your first note.'));
                  }
                  return ListView(
                    children: snapshot.data!.map((n) {
                      return noteBuilder(n);
                    }).toList(),
                  );
                },
              ),
            ),
          ),
        ],
      ),
    );
  }
}
