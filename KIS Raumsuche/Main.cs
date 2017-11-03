using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace TUK_Raumsuche
{
    public partial class Main : Form
    {
        class Room
        {
            public String   oid = "",
                            name = "";
            public String places = "";
            public String group = "";
            public bool free = false;
        }

        class clb_item
        {
            public String display = "", value = "";

            public override string ToString()
            {
                return display;
            }
        }

        Thread search_worker;
        DateTime begin, end;
        String status = "Idle";

        List<Room> allRooms = new List<Room>();

        public Main()
        {
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (search_worker != null && search_worker.IsAlive) return;

            search_worker = new Thread(new ThreadStart(thread_search));
            search_worker.IsBackground = true;
            search_worker.Start();
        }

        private void thread_search()
        {
            //String html = sendRequest("https://www.kis.uni-kl.de/campus/all/roomGroups.asp", null);

            if (allRooms.Count == 0)
            {
                status = "Do the setup first!";
                return;
            }

            pb_search.BeginInvoke(new MethodInvoker(() =>
            {
                pb_search.Value = 0;
                pb_search.Maximum = allRooms.Count;
            }));
            dgv_results.BeginInvoke(new MethodInvoker(() =>
            {
                dgv_results.Rows.Clear();
            }));

            begin = new DateTime(
                dtp_date.Value.Year, 
                dtp_date.Value.Month, 
                dtp_date.Value.Day, 
                dtp_startTime.Value.Hour, 
                dtp_startTime.Value.Minute,
                dtp_startTime.Value.Second);
            end = new DateTime(
                dtp_date.Value.Year,
                dtp_date.Value.Month,
                dtp_date.Value.Day,
                dtp_endTime.Value.Hour,
                dtp_endTime.Value.Minute,
                dtp_endTime.Value.Second);

            List<Thread> threads = new List<Thread>();

            status = "Checking rooms (" + allRooms.Count + ")...";

            foreach (Room room in allRooms)
            {
                if (!isGroupChecked(room.group))
                {
                    pb_search.BeginInvoke(new MethodInvoker(() =>
                    {
                        pb_search.Value++;
                    }));
                    continue;
                }

                Thread t = new Thread(new ParameterizedThreadStart(thread_room));
                t.IsBackground = true;
                t.Start(room);
                threads.Add(t);

                /*status = "Checking room " + room.name + " (" + room.oid + ")";
                room.free = isRoomFree(room);
                if (room.free)
                {
                    dgv_results.BeginInvoke(new MethodInvoker(() =>
                    {
                        dgv_results.Rows.Add(room.oid, room.name, room.places);
                    }));
                }
                pb_search.BeginInvoke(new MethodInvoker(() =>
                {
                    pb_search.Value++;
                }));*/
            }

            foreach (Thread t in threads)
                t.Join();

            status = "Done";
        }

        private bool isGroupChecked(String value)
        {
            foreach (clb_item item in clb_roomGroups.CheckedItems)
            {
                if (item.value == value) return true;
            }
            return false;
        }

        private List<String> getRoomGroups()
        {
            Uri baseUri = new Uri("https://www.kis.uni-kl.de/campus/all/roomGroups.asp");

            List<String> roomGroups = new List<String>();

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(baseUri);
            /*HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(sendRequest(baseUri, null));*/

            List<HtmlNode> links = 
                doc.DocumentNode.Descendants("a")
                .Where(x => x.GetAttributeValue("name", "") != "").ToList();
            foreach (HtmlNode link in links)
            {
                String href = link.GetAttributeValue("href", "");
                Uri uri = new Uri(baseUri, href);
                foreach (String s in uri.Query.Substring(1).Replace("&amp;", "&").Split('&'))
                    // .Substring(1) removes the '?'
                {
                    String[] parts = s.Split('=');
                    if (parts[0] == "expand")
                    {
                        roomGroups.Add(parts[1]);
                    }
                }
            }

            return roomGroups;
        }

        private List<Room> getRooms(String roomGroup)
        {
            Uri baseUri = new Uri("https://www.kis.uni-kl.de/campus/all/roomGroups.asp?view=true&expand=" + roomGroup);

            List<Room> rooms = new List<Room>();

            /*HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(baseUri);*/
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(sendRequest(baseUri, null));

            List<HtmlNode> tables = 
                doc.DocumentNode.Descendants("table")
                .Where(x => x.GetAttributeValue("class", "") == "Print")
                .ToList();

            // should be only one but who knows ^^
            foreach (HtmlNode table in tables)
            {
                List<HtmlNode> rows = table.Descendants("tr").ToList();
                // drop table head
                rows.RemoveAt(0);

                foreach (HtmlNode row in rows)
                {
                    Room room = new Room();
                    room.group = roomGroup;
                    List<HtmlNode> cells = row.Descendants("td").ToList();
                    if (cells.Count < 5) continue;
                    room.places = cells[1].InnerHtml;
                    List<HtmlNode> links = cells[4].Descendants("a").ToList();
                    // should be again only one
                    String href = links[0].GetAttributeValue("href", "");
                    Uri uri = new Uri(baseUri, href);
                    Console.Out.WriteLine(uri.Query);
                    foreach (String s in uri.Query.Substring(1).Replace("&amp;", "&").Split('&'))
                    {
                        String[] parts = s.Split('=');
                        if (parts[0] == "room")
                        {
                            room.name = parts[1];
                        }
                        else if (parts[0] == "oid")
                        {
                            room.oid = parts[1];
                        }
                    }
                    rooms.Add(room);
                }
            }

            return rooms;
        }

        private void thread_room(object room)
        {
            Room r = (Room)room;
            r.free = isRoomFree(r);
            if (r.free)
            {
                dgv_results.BeginInvoke(new MethodInvoker(() =>
                {
                    dgv_results.Rows.Add(r.oid, r.name, r.places);
                }));
            }
            pb_search.BeginInvoke(new MethodInvoker(() =>
            {
                pb_search.Value++;
            }));
        }

        private bool isRoomFree(Room room)
        {
            Uri baseUri = new Uri("https://www.kis.uni-kl.de/campus/all/roomDate.asp?room="+room.name+"&oid="+room.oid+"&date="+begin.ToString("dd.MM.yyyy"));

            List<String> roomGroups = new List<String>();

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(baseUri);
            /*HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(sendRequest(baseUri, null));*/

            List<HtmlNode> contents = 
                doc.DocumentNode
                .Descendants("content").ToList()[0] // should be only one
                .Ancestors("div").ToList()[0] // should be only one
                .Descendants("div").ToList();

            List<HtmlNode> header = contents.Where(
                x =>
                {
                    String[] styles = x.GetAttributeValue("style", "").Replace(" ", "").Split(';');

                    foreach (String style in styles)
                    {
                        String[] parts = style.Split(':');
                        if (parts[0] == "top")
                        {
                            try
                            {
                                int t = int.Parse(parts[1].Replace("px", ""));
                                return (t == 0); // is header
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }

                    return false;
                }).ToList();

            int day = (int)begin.DayOfWeek - 1;
            if (day == -1) day = 6;

            int pixelDelta = 140;

            if (header.Count == 7)
            {
                pixelDelta = 120;
            }
            else if (header.Count == 8)
            {
                pixelDelta = 110;
            }

            // contents now contains all sorts of elements, we need to filter by style attribute
            List<HtmlNode> units = contents.Where(
                x =>
                {
                    String[] styles = x.GetAttributeValue("style", "").Replace(" ", "").Split(';');

                    foreach (String style in styles)
                    {
                        String[] parts = style.Split(':');
                        if (parts[0] == "left")
                        {
                            try
                            {
                                int l = int.Parse(parts[1].Replace("px", ""));
                                return 
                                    (l % 10 == 8) && // is foreground element
                                    ((l - 58) / pixelDelta == day); // is on correct day
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }

                    return false;
                }).ToList();

            // matches dd:dd-dd:dd where d is any number e.g. 09:00-13:00 (times :D)
            Regex regex = new Regex("[0-9]{2}:[0-9]{2}-[0-9]{2}:[0-9]{2}");

            foreach (HtmlNode unit in units)
            {
                Match match = regex.Match(unit.InnerHtml);
                if (match.Success)
                {
                    String[] times = match.Value.Split('-');
                    String[] parts = times[0].Split(':');
                    DateTime unit_begin = new DateTime(
                        begin.Year,
                        begin.Month,
                        begin.Day,
                        int.Parse(parts[0]),
                        int.Parse(parts[1]),
                        0);
                    parts = times[1].Split(':');
                    DateTime unit_end = new DateTime(
                        begin.Year,
                        begin.Month,
                        begin.Day,
                        int.Parse(parts[0]),
                        int.Parse(parts[1]),
                        0);

                    if (!(unit_end < begin || unit_begin > end)) return false;
                }
            }

            return true;
        }

        private void dgv_results_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Process.Start(
                "https://www.kis.uni-kl.de/campus/all/roomDate.asp?room=" +
                dgv_results.Rows[e.RowIndex].Cells["room_name"].Value + 
                "&oid=" + dgv_results.Rows[e.RowIndex].Cells["room_id"].Value + 
                "&date=" + begin.ToString("dd.MM.yyyy"));
        }

        private void timer_status_Tick(object sender, EventArgs e)
        {
            lbl_status.Text = status;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new About().ShowDialog();
        }

        private void btn_setup_Click(object sender, EventArgs e)
        {
            if (search_worker != null && search_worker.IsAlive) return;

            search_worker = new Thread(new ThreadStart(thread_setup));
            search_worker.IsBackground = true;
            search_worker.Start();
        }

        private void thread_setup()
        {
            status = "Preparing...";

            btn_setup.BeginInvoke(new MethodInvoker(() =>
            {
                btn_setup.Hide();
            }));

            List<String> roomGroups = getRoomGroups();

            clb_roomGroups.BeginInvoke(new MethodInvoker(() =>
            {
                foreach (String group in roomGroups)
                {
                    clb_item i = new clb_item();
                    i.value = group;
                    i.display = WebUtility.UrlDecode(group);
                    clb_roomGroups.Items.Add(i, true);
                }
            }));

            status = "Got " + roomGroups.Count + " room groups...";

            foreach (String roomGroup in roomGroups)
            {
                status = "Checking room group: " + roomGroup;
                List<Room> r = getRooms(roomGroup);
                allRooms.AddRange(r);
            }

            status = "Setup comlpete! Got " + allRooms.Count + " rooms.";
        }

        private String sendRequest(Uri url, String post)
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            if (post != null)
            {
                wr.Method = "POST";
                wr.ContentType = "application/x-www-form-urlencoded";
                byte[] bytes = Encoding.UTF8.GetBytes(post);
                wr.ContentLength = bytes.Length;
                Stream request = wr.GetRequestStream();
                request.Write(bytes, 0, bytes.Length);
                request.Close();
            }
            else
            {
                wr.Method = "GET";
            }

            HttpWebResponse response = (HttpWebResponse)wr.GetResponse();
            Stream r = response.GetResponseStream();
            StreamReader reader = new StreamReader(r);
            String ret = reader.ReadToEnd();
            reader.Close();
            r.Close();
            response.Close();

            return ret;
        }
    }
}
