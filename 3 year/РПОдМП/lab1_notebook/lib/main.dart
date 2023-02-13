import "package:flutter/material.dart";
import 'package:lab1_notebook/pages/home_page.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(const NotebookApp());
}

class NotebookApp extends StatelessWidget {
  const NotebookApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Notebook',
      theme: ThemeData(
          colorScheme: const ColorScheme(
            error: Colors.red,
            brightness: Brightness.dark,
            background: Color(0xFF212121),
            onBackground: Color(0xFF212121),
            onError: Colors.red,
            onPrimary: Color(0xFF212121),
            onSecondary: Color.fromARGB(255, 11, 11, 11),
            onSurface: Color(0xFF212121),
            primary: Color(0xFF212121),
            secondary: Color.fromARGB(255, 11, 11, 11),
            surface: Color(0xFF212121),
          ),
          appBarTheme: const AppBarTheme(
            foregroundColor: Color.fromARGB(255, 205, 205, 205),
            backgroundColor: Color.fromARGB(255, 11, 11, 11),
          ),
          floatingActionButtonTheme: const FloatingActionButtonThemeData(
            foregroundColor: Colors.white,
            backgroundColor: Color.fromARGB(255, 53, 53, 53),
          )),
      debugShowCheckedModeBanner: false,
      home: const NotebookHomePage(),
    );
  }
}
