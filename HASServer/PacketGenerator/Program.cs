using System;
using System.IO;
using System.Xml;

namespace PacketGenerator
{
    public enum PacketType
    {
        None,
        Packet,
        DataPacket
    }
    class Program
    {
        static string genPackets;
        static ushort packetId;
        static string packetEnums;

        static string clientRegister;
        static string serverRegister;

        static void Main(string[] args)
        {
            string pdlPath = "../PDL.xml";

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            if (args.Length >= 1)
                pdlPath = args[0];

            using (XmlReader r = XmlReader.Create(pdlPath, settings))
            {
                r.MoveToContent();

                while (r.Read())
                {
                    if (r.Depth == 1 && r.NodeType == XmlNodeType.Element)
                    {

                        ParsePacket(r);
                    }
                }

                string fileText = string.Format(PacketFormat.fileFormat, packetEnums, genPackets);
                File.WriteAllText("GenPackets.cs", fileText);
                string clientManagerText = string.Format(PacketFormat.managerFormat, clientRegister);
                File.WriteAllText("ClientPacketManager.cs", clientManagerText);
                string serverManagerText = string.Format(PacketFormat.managerFormat, serverRegister);
                File.WriteAllText("ServerPacketManager.cs", serverManagerText);
            }
        }

        public static void ParsePacket(XmlReader r)
        {
            if (r.NodeType == XmlNodeType.EndElement)
                return;

            bool isDataPacket = r.Name.ToLower() == "datapacket";

            string packetName = r["name"];
            if (string.IsNullOrEmpty(packetName))
            {
                Console.WriteLine("Packet without name");
                return;
            }

            Tuple<string, string, string> t = ParseMembers(r);
            if (isDataPacket)
                genPackets += string.Format(PacketFormat.dataPacketFormat, packetName, t.Item1, t.Item2, t.Item3);
            else
            {
                genPackets += string.Format(PacketFormat.packetFormat, packetName, t.Item1, t.Item2, t.Item3);
                packetEnums += string.Format(PacketFormat.packetEnumFormat, packetName, ++packetId) + Environment.NewLine + "\t";
            }

            if (packetName.StartsWith("S_") || packetName.StartsWith("s_"))
                clientRegister += string.Format(PacketFormat.managerRegisterFormat, packetName) + Environment.NewLine;
            else if (packetName.StartsWith("C_") || packetName.StartsWith("c_"))
                serverRegister += string.Format(PacketFormat.managerRegisterFormat, packetName) + Environment.NewLine;
        }

        // {1} 멤버 변수들
        // {2} 멤버 변수 Read
        // {3} 멤버 변수 Write
        public static Tuple<string, string, string> ParseMembers(XmlReader r)
        {
            string packetName = r["name"];

            string memberCode = "";
            string readCode = "";
            string writeCode = "";

            int depth = r.Depth + 1;
            while (r.Read())
            {
                Console.WriteLine(r.Name + " " + r.Depth + ' ' + r.NodeType);
                if (r.Depth != depth)
                    break;
                if (r.NodeType == XmlNodeType.EndElement)
                    continue;

                string memberName = r["name"];
                if (string.IsNullOrEmpty(memberName))
                {
                    Console.WriteLine("Member without name");
                    return null;
                }

                if (string.IsNullOrEmpty(memberCode) == false)
                    memberCode += Environment.NewLine;
                if (string.IsNullOrEmpty(readCode) == false)
                    readCode += Environment.NewLine;
                if (string.IsNullOrEmpty(writeCode) == false)
                    writeCode += Environment.NewLine;

                string memberType = r.Name;
                string changedType = FirstCharToUpper(memberType);
                switch (memberType)
                {
                    case "byte":
                    case "sbyte":
                    case "bool":
                    case "short":
                    case "ushort":
                    case "int":
                    case "long":
                    case "float":
                    case "double":
                    case "string":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readFormat, changedType, memberName);
                        writeCode += string.Format(PacketFormat.writeFormat, changedType, memberName);
                        break;
                    case "list":
                        memberCode += string.Format(PacketFormat.memberListFormat, GetListMember(r), memberName);
                        readCode += string.Format(PacketFormat.readFormat, changedType, memberName);
                        writeCode += string.Format(PacketFormat.writeFormat, changedType, memberName);
                        break;
                    default:
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readFormat, "DataPacket", memberName);
                        writeCode += string.Format(PacketFormat.writeFormat, "DataPacket", memberName);
                        break;
                }
            }

            memberCode = memberCode.Replace("\n", "\n\t");
            readCode = readCode.Replace("\n", "\n\t\t");
            writeCode = writeCode.Replace("\n", "\n\t\t");
            return new Tuple<string, string, string>(memberCode, readCode, writeCode);
        }
        public static string GetListMember(XmlReader r)
        {
            r.Read();
            return FirstCharToUpper(r.Name);
        }
        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        public static string FirstCharToLower(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            return input[0].ToString().ToLower() + input.Substring(1);
        }
    }
}
