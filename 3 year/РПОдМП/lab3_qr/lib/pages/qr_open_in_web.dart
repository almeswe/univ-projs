import 'package:flutter/material.dart';
import 'package:webview_flutter/webview_flutter.dart';

class QrOpenInWebPage extends StatefulWidget {
  final String? url;

  const QrOpenInWebPage({super.key, @required this.url});

  @override
  State<QrOpenInWebPage> createState() {
    // ignore: no_logic_in_create_state
    return _QrOpenInWebPageState(url: url);
  }
}

class _QrOpenInWebPageState extends State<QrOpenInWebPage> {
  final String? url;
  late final WebViewController controller;

  _QrOpenInWebPageState({@required this.url});

  @override
  void initState() {
    super.initState();
    controller = WebViewController()
      ..loadRequest(
        Uri.parse(url!),
      );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: WebViewWidget(
        controller: controller,
      ),
    );
  }
}
