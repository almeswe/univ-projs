import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:lab3_qr/pages/qr_home.dart';

import 'package:lab3_qr/pages/qr_open_in_web.dart';
import 'package:lab3_qr/widgets/qr_app_button.dart';

class QrScanShowPageBody extends StatelessWidget {
  final String? value;

  const QrScanShowPageBody({super.key, @required this.value});

  void _onCopyToClip(String value) async {
    await Clipboard.setData(
      ClipboardData(text: value),
    );
  }

  void _onOpenInWeb(BuildContext context, String url) async {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) {
          return QrOpenInWebPage(
            url: url,
          );
        },
      ),
    );
  }

  Widget _getRawText() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        SizedBox(
          width: 350,
          height: 400,
          child: Container(
            decoration: BoxDecoration(
              color: const Color.fromARGB(255, 125, 205, 184),
              border: Border.all(
                width: 2,
                color: const Color.fromARGB(255, 95, 155, 134),
              ),
              borderRadius: const BorderRadius.all(
                Radius.circular(9.0),
              ),
            ),
            child: Align(
              alignment: Alignment.topCenter,
              child: Text(
                value!,
                style: const TextStyle(
                  fontSize: 25,
                  color: Color.fromARGB(255, 245, 245, 245),
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
        ),
      ],
    );
  }

  Widget _getCopyButton(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(
        bottom: 10.0,
        top: 10.0,
      ),
      child: QrAppButton.sized(
        width: 350,
        height: 40,
        text: 'Copy',
        onPressed: () {
          _onCopyToClip(value!);
        },
      ),
    );
  }

  Widget _getOpenInWebButton(BuildContext context) {
    if (!value!.contains('http')) {
      return const SizedBox(height: 1);
    }
    return QrAppButton.sized(
      width: 350,
      height: 40,
      text: 'Open in Web',
      onPressed: () {
        _onOpenInWeb(context, value!);
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        _getRawText(),
        _getCopyButton(context),
        _getOpenInWebButton(context),
      ],
    );
  }
}

class QrScanShowPage extends StatelessWidget {
  final String? value;

  const QrScanShowPage({super.key, @required this.value});

  Future<bool> _onBackPressed(BuildContext context) async {
    Navigator.pushReplacement(
      context,
      MaterialPageRoute(
        builder: (context) {
          return const HomePage();
        },
      ),
    );
    return true;
  }

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: () async {
        return await _onBackPressed(context);
      },
      child: Scaffold(
        body: QrScanShowPageBody(value: value),
        backgroundColor: const Color.fromARGB(255, 185, 255, 234),
      ),
    );
  }
}
