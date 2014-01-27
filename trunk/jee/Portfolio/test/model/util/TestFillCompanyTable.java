package model.util;

import java.io.IOException;

import properties.Configuration;

public class TestFillCompanyTable {
	public static void main(String [] args) throws IOException{
		
		new FillCompanyTable(Configuration.getInstance().getCompanyDirectoryName(), 2);
		System.err.println("End");
	}
}
