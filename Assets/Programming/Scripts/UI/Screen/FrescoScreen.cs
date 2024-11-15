using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;
using UnityEngine.Networking;

public class FrescoScreen : ScreenUI
{
	public static Action OnBack;
	public static Action OnCompo;

	[SerializeField] private string _bookUrl;
	[SerializeField] private string _videoUrl;
	[SerializeField] private string _archiveUrl;

	[SerializeField] private AnimatedButton _back;
	[SerializeField] private AnimatedButton _compo;
	[SerializeField] private AnimatedButton _book;
	[SerializeField] private AnimatedButton _video;
	[SerializeField] private AnimatedButton _archiche;

	protected override void Start()
	{
		_back.OnPlay += Back;
		_compo.OnPlay += Compo;
		_book.OnPlay += Book;
		_video.OnPlay += Video;
		_archiche.OnPlay += Archive;
	}

	private void Back() { OnBack?.Invoke(); }
	private void Compo() { OnCompo?.Invoke(); }
	private void Book()
	{
		print("book");
		Application.OpenURL(_bookUrl);
	}

	private void Video()
	{
		print("video");
        Application.OpenURL(_videoUrl);
    }

	private void Archive()
	{
		print("archive");
        Application.OpenURL(_archiveUrl);
    }

    protected override void OnDestroy()
    {
        _back.OnPlay -= Back;
        _compo.OnPlay -= Compo;
        _book.OnPlay -= Book;
        _video.OnPlay -= Video;
        _archiche.OnPlay -= Archive;
    }
}