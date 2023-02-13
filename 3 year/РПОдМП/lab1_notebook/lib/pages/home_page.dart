import "package:flutter/material.dart";

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
  var noteTitleStyle = const TextStyle(
    fontSize: 25,
    fontFamily: 'RobotoBold',
  );
  var noteSubTitleStyle = const TextStyle(
    fontSize: 13,
    fontFamily: 'RobotoThin',
    color: Color(0xFFD3D3D3),
  );

  Widget noteBuilder(Note note) {
    var theme = Theme.of(context);
    return Row(
      children: [
        ListTile(
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
                  return NotebookEditPage(note: note);
                },
              ),
            );
          },
        ),
        const SizedBox(height: 15)
      ],
    );
  }

  void noteNew() {
    var date = DateTime.now();
    var dateStr = "${date.day}-${date.month}-${date.year}";
    setState(() {
      notes.add(Note(dateStr, "New Note"));
    });
  }

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
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
        onPressed: () {
          noteNew();
        },
        child: const Icon(Icons.add),
      ),
      body: Container(
        padding: const EdgeInsets.only(left: 25.0, right: 25.0),
        child: Padding(
          padding: const EdgeInsets.only(top: 15.0),
          child: FutureBuilder(
            future: NotesDatabase.instance.fetchNotes(),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: Text('Write your first note.'));
              }
              return ListView(
                children: snapshot.data!.map((n) => noteBuilder(n)).toList(),
              );
            },
          ),
        ),
      ),
    );
  }
}
