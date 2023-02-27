import 'package:html/parser.dart';
import 'package:http/http.dart' as http;
import 'package:webfeed/webfeed.dart';

class LifehackerParagraph {
  var header = "About";
  var contents = <String>[];
}

class LifehackerItemModel {
  String? link;
  late String title;
  late String subtitle;
  late int statusCode;
  var paragraphs = <LifehackerParagraph>[];

  LifehackerItemModel(RssItem basedOnRss) {
    title = "";
    subtitle = "";
    link = basedOnRss.link;
  }

  Future<int> fetchItem() async {
    var client = http.Client();
    var response = await client.get(Uri.parse(link!));
    statusCode = response.statusCode;
    if (response.statusCode == 200) {
      var document = parse(response.body);
      var paragraph = LifehackerParagraph();
      for (var container
          in document.getElementsByClassName('sc-1efpnfq-0 joZwQS')) {
        title = container.text;
      }
      for (var container in document
          .getElementsByClassName('sc-1xcxnn7-0 glTfBX js_regular-subhead')) {
        subtitle = container.text;
      }
      for (var container in document
          .getElementsByClassName('xs32fe-0 iOFxrO js_post-content')[0]
          .children) {
        if (container.localName == "p") {
          paragraph.contents.add(container.text);
        }
        if (container.localName == "h2" || container.localName == "h3") {
          paragraphs.add(paragraph);
          paragraph = LifehackerParagraph();
          paragraph.header = container.text;
        }
      }
      paragraphs.add(paragraph);
    }
    return 0;
  }
}
