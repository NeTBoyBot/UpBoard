using AutoMapper;
using Board.Contracts.File;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Application.AppData.Contexts.File.Repositories;
using UpBoard.Contracts.File;

namespace UpBoard.Application.AppData.Contexts.File.Services
{
    ///<inheritdoc cref="IFileService"/>
    public class FileService : IFileService
    {
        public readonly IFileRepository _fileRepository;
        public readonly IMapper _mapper;

        public FileService(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        ///<inheritdoc/>
        public async Task<Guid> CreateFileAsync(CreateFileRequest createAd, CancellationToken cancellation)
        {
            var AdId = await _fileRepository.AddAsync(createAd, cancellation);

            return AdId;
        }
        ///<inheritdoc/>
        public async Task DeleteAsync(DeleteFileRequest request, CancellationToken cancellation)
        {

            await _fileRepository.DeleteAsync(request, cancellation);
        }
        

        ///<inheritdoc/>
        public async Task<InfoFileResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _fileRepository.FindById(id, cancellation);
            return _mapper.Map<InfoFileResponse>(existingad);
        }
    }
}
