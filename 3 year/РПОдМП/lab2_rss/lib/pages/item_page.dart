import 'package:webfeed/webfeed.dart';
import 'package:flutter/material.dart';
import 'package:webview_flutter/webview_flutter.dart';

import 'package:lab2_rss/rss/lifehacker_rss_model.dart';

class RssReaderItemPage extends StatefulWidget {
  RssItem item;
  RssReaderItemPage({super.key, required this.item});

  @override
  State<RssReaderItemPage> createState() => _RssReaderItemPageState(item: item);
}

class _RssReaderItemPageState extends State<RssReaderItemPage> {
  RssItem item;
  late final WebViewController controller;

  _RssReaderItemPageState({required this.item});

  @override
  void initState() {
    super.initState();
    controller = WebViewController()
      ..loadRequest(
        Uri.parse(item.link!),
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
