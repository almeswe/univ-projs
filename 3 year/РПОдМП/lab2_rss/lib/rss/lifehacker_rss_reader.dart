import 'package:http/http.dart';
import 'package:webfeed/webfeed.dart';

class LifehackerReader {
  static const String baseUrl = 'https://lifehacker.com/rss';
  static List<RssItem> _fetchedItems = <RssItem>[];

  static Future<RssFeed> fetchRssFeed() async {
    var client = Client();
    var response = await client.get(Uri(
      scheme: 'https',
      host: 'lifehacker.com',
      port: 443,
      path: '/rss',
    ));
    return RssFeed.parse(response.body);
  }

  static Future<List<RssItem>> fetchItems() async {
    var feed = await fetchRssFeed();
    if (feed.items != null) {
      _fetchedItems = feed.items!;
    }
    return _fetchedItems;
  }

  static int getItemCount() {
    return _fetchedItems.length;
  }

  static RssItem getItem(int index) {
    return _fetchedItems[index];
  }
}
