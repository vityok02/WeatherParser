﻿using Application.Common.Interfaces;

namespace Application.Common.Abstract;

public class FileWrapper : IFile
{
    private readonly Stream _stream;

    public FileWrapper(Stream stream)
    {
        _stream = stream;
    }

    public Stream GetStream()
    {
        return _stream;
    }
}
