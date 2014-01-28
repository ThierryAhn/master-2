package model.util;

import java.io.BufferedInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.net.URLConnection;

import properties.Configuration;

/**
 * Class Download which helps to download file on Internet
 * @author ABALINE & AHOUNOU
 * 15 janv. 2014
 */
public class Download
{
	/**
	 * Get file from internet
	 * @param host host of the file
	 * @throws IOException 
	 */
	public static void getFile(String host) throws IOException
	{
		
		URL url = new URL(host);
		BufferedInputStream buf = new BufferedInputStream(url.openStream());
		byte[] buffer = new byte[1024];
		
		int read;
		FileOutputStream writeFile = null;
		
		// construct file name
		String filename = host.split("exchange=")[1].split("&")[0] +".csv";
		
		writeFile = new FileOutputStream(Configuration.getInstance().getCompanyDirectoryName()+"/" +filename);
		
		while ((read = buf.read(buffer)) > 0){
			writeFile.write(buffer, 0, read);
		}
		writeFile.flush();
		writeFile.close();
	}

	public static void main(String[] args) throws IOException
	{
		Download.getFile("http://www.nasdaq.com/screening/companies-by-industry.aspx?exchange=ametotjteitn&render=download");
		
		System.out.println("end");
	}
}
