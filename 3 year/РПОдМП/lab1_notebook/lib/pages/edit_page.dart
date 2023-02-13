import "package:flutter/material.dart";
import "package:lab1_notebook/models/note.dart";

class NotebookEditPage extends StatelessWidget {
  final Note note;

  const NotebookEditPage({super.key, required this.note});

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
    return Scaffold(
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
                onPressed: () {
                  Navigator.pop(context);
                },
              ),
              Expanded(
                child: TextField(
                  controller: TextEditingController(
                    text: note.title,
                  ),
                  style: const TextStyle(
                    fontSize: 40,
                    fontFamily: "Montserrat",
                  ),
                  decoration: const InputDecoration(
                    border: InputBorder.none,
                    hintText: 'Write a title',
                  ),
                ),
              )
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
                controller: TextEditingController(
                  text: note.contents,
                ),
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
