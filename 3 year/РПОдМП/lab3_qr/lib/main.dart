import 'package:flutter/material.dart';
import 'package:lab3_qr/pages/qr_home.dart';

void main() {
  runApp(const MaterialApp(home: QrScannerApp()));
}

class QrScannerApp extends StatelessWidget {
  const QrScannerApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const HomePage();
  }
}
