import 'package:flutter/material.dart';
import 'package:lab3_qr/pages/qr_scan_show.dart';
import 'package:mobile_scanner/mobile_scanner.dart';

class QrScanPage extends StatelessWidget {
  var _qrShown = false;
  final cameraController = MobileScannerController(facing: CameraFacing.back);

  QrScanPage({super.key});

  void _onDetect(BuildContext context, BarcodeCapture capture) {
    final List<Barcode> barcodes = capture.barcodes;
    if (barcodes.isNotEmpty) {
      final barcode = barcodes[0];
      final barcodeValue = barcode.rawValue == null ? '' : barcode.rawValue!;
      if (barcodeValue != '' && !_qrShown) {
        _qrShown = true;
        cameraController.stop();
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (context) {
              return QrScanShowPage(
                value: barcodeValue,
              );
            },
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: MobileScanner(
        controller: cameraController,
        onDetect: (capture) {
          _onDetect(context, capture);
        },
      ),
    );
  }
}
