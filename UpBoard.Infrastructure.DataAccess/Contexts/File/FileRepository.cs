using AutoMapper;
using Board.Contracts.File;
using UpBoard.Application.AppData.Contexts.File.Repositories;
using UpBoard.Contracts.File;
using UpBoard.Infrastucture.Repository;

namespace Board.Infrastucture.DataAccess.Contexts.File
{
    ///<inheritdoc cref="IFileRepository"/>
    public class FileRepository : IFileRepository
    {
        public IRepository<Domain.File> _baseRepository;
        private readonly IMapper _mapper;

        public FileRepository(IRepository<Domain.File> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public Task<Guid> AddAsync(CreateFileRequest model, CancellationToken cancellation)
        {
            var file = _mapper.Map<Domain.File>(model);
            _baseRepository.AddAsync(file, cancellation);
            return Task.Run(()=> file.Id);
        }

        ///<inheritdoc/>
        public Task DeleteAsync(DeleteFileRequest model, CancellationToken cancellation)
        {
            var file = _mapper.Map<Domain.File>(model);
            return _baseRepository.DeleteAsync(file, cancellation);
        }

        ///<inheritdoc/>
        public async Task<InfoFileResponse> FindById(Guid id, CancellationToken cancellation)
        {
            var file = await _baseRepository.GetByIdAsync(id, cancellation); ;
            return _mapper.Map<InfoFileResponse>(file);
        }

        
    }
}
