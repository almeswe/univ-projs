import 'package:flutter/material.dart';

import 'package:lab3_qr/pages/qr_gen.dart';
import 'package:lab3_qr/pages/qr_scan.dart';
import 'package:lab3_qr/widgets/qr_app_button.dart';

class HomePageBody extends StatelessWidget {
  const HomePageBody({super.key});

  void _onQrGeneratePressed(BuildContext context) {
    Navigator.push(
      context,
      MaterialPageRoute(builder: (context) {
        return const QrGeneratorPage();
      }),
    );
  }

  void _onQrScanPressed(BuildContext context) {
    Navigator.push(
      context,
      MaterialPageRoute(builder: (context) {
        return QrScanPage();
      }),
    );
  }

  Widget _getScanImage() {
    return const Image(
      image: AssetImage('assets/images/qr.jpg'),
    );
  }

  Widget _getGeneratorButton(BuildContext context) {
    return QrAppButton(
      text: 'Generate QR',
      onPressed: () {
        _onQrGeneratePressed(context);
      },
    );
  }

  Widget _getScanButton(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 10.0),
      child: QrAppButton(
        text: 'Scan QR',
        onPressed: () {
          _onQrScanPressed(context);
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
          _getScanImage(),
          _getScanButton(context),
          _getGeneratorButton(context),
        ],
      ),
    );
  }
}

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      backgroundColor: Color.fromARGB(255, 185, 255, 234),
      body: HomePageBody(),
    );
  }
}
