package br.com.marcomvidal.buscatalog.scraper.synchronization;

import org.springframework.data.jpa.repository.JpaRepository;

import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SynchronizedLine;

public interface SynchronizedLineRepository extends JpaRepository<SynchronizedLine, Long> {}
