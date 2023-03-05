import 'package:flutter/material.dart';
import 'package:qr_flutter/qr_flutter.dart';

import 'package:lab3_qr/widgets/qr_app_button.dart';

class QrGeneratorPageBody extends StatefulWidget {
  const QrGeneratorPageBody({super.key});

  @override
  State<QrGeneratorPageBody> createState() => _QrGeneratorPageBodyState();
}

class _QrGeneratorPageBodyState extends State<QrGeneratorPageBody> {
  var _qrData = 'initial';
  final _textController = TextEditingController();

  void _onGenerate(String basedOnText) {
    setState(() {
      _qrData = basedOnText;
    });
  }

  Widget _getQrImage(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 10.0),
      child: QrImage(
        size: 270.0,
        data: _qrData,
        version: QrVersions.auto,
      ),
    );
  }

  Widget _getTextInput(BuildContext context) {
    return SizedBox(
      width: 250,
      height: 40,
      child: Container(
        color: const Color.fromARGB(255, 165, 235, 214),
        child: TextField(
          controller: _textController,
          decoration: const InputDecoration(
            border: OutlineInputBorder(),
            labelText: 'Enter text',
          ),
        ),
      ),
    );
  }

  Widget _getGenerateButton(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 10.0),
      child: QrAppButton.sized(
        width: 250,
        height: 40,
        text: 'Generate QR',
        onPressed: () {
          _onGenerate(_textController.text);
        },
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          _getQrImage(context),
          _getTextInput(context),
          _getGenerateButton(context),
        ],
      ),
    );
  }
}

class QrGeneratorPage extends StatelessWidget {
  const QrGeneratorPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: QrGeneratorPageBody(),
      backgroundColor: const Color.fromARGB(255, 185, 255, 234),
    );
  }
}
