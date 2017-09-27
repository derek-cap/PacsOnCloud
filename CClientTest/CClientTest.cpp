// CClientTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "Poco/Net/StreamSocket.h"   //流式套接字
#include "Poco/Net/SocketStream.h"   //套接字流
#include "Poco/Net/SocketAddress.h"   //套接字地址
#include "Poco/StreamCopier.h"  //流复制器
#include "Poco/Path.h"   //路径解析器
#include "Poco/Exception.h"  //异常
#include "Poco/Net/HTTPClientSession.h"
#include "Poco/Net/HTTPRequest.h"
#include "Poco/Net/HTTPResponse.h"
#include <Poco/Net/HTTPCredentials.h>
#include "Poco/NullStream.h"
#include "Poco/URI.h"

using Poco::Net::HTTPClientSession;
using Poco::Net::HTTPRequest;
using Poco::Net::HTTPResponse;
using Poco::Net::HTTPMessage;
using Poco::Net::StreamSocket;
using Poco::Net::SocketStream;
using Poco::Net::SocketAddress;
using Poco::StreamCopier;
using Poco::Path;
using Poco::URI;
using Poco::Exception;

bool doRequest(Poco::Net::HTTPClientSession& session, Poco::Net::HTTPRequest& request, Poco::Net::HTTPResponse& response)
{
	session.sendRequest(request);
	std::istream& rs = session.receiveResponse(response);
	std::cout << response.getStatus() << " " << response.getReason() << std::endl;
	if (response.getStatus() != Poco::Net::HTTPResponse::HTTP_UNAUTHORIZED)
	{
		StreamCopier::copyStream(rs, std::cout);
		return true;
	}
	else
	{
		Poco::NullOutputStream null;
		StreamCopier::copyStream(rs, null);
		return false;
	}
}


int main()
{
	std::cout << "hello" << std::endl;

	const std::string HOST("dict.org");
	const unsigned short PORT = 2628;
	std::string term("A");

	try
	{
		//SocketAddress sa(HOST, PORT);
		//StreamSocket sock(sa);
		//SocketStream str(sock);

		//str << "DEFINE ! " << term << "\r\n" << std::flush;
		//str << "QUIT\r\n" << std::flush;

		//sock.shutdownSend();
		//// Writes all bytes readable from istr to ostr, using an internal buffer.
		//// Returns the number of bytes copied.
		//StreamCopier::copyStream(str, std::cout);
		URI uri("http://www.baidu.com");
		std::string path(uri.getPathAndQuery());
		if (path.empty())
			path = "/";

		HTTPClientSession session(uri.getHost(), uri.getPort());
		HTTPRequest request(HTTPRequest::HTTP_GET, path, HTTPMessage::HTTP_1_1);
		HTTPResponse response;
		if (!doRequest(session, request, response))
		{

		}
	}
	catch (Exception& ex)
	{
		std::cerr << ex.displayText() << std::endl;
	}

	char inputString = 0;
	std::cin >> inputString;
    return 0;
}

